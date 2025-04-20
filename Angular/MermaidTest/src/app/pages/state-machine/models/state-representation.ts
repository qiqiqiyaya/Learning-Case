import { Observable, Subscription, combineLatest, merge, mergeAll, of } from "rxjs";
import { Transition } from "./transition";
import { BaseFunc } from "./base-func";

export class StateRepresentation extends BaseFunc<StateRepresentation> {
    private _mergeSub: Subscription;
    private _state: string;
    private _transitions: Transition[] = [];

    constructor(state?: string) {
        super();
        if (state) this._state = state;
    }

    get State() {
        return this._state;
    }
    set State(value: string) {
        this._state = value;
        this.setJsonProperty("State", value);
        this.valueChange.emit(this);
    }

    get transitionGroup(): any {
        if (this.Transitions.length == 0) return {};
        const obj = this.Transitions.reduce(function (r, a) {
            r[a.Trigger] = r[a.Trigger] || [];
            r[a.Trigger].push(a);
            return r;
        }, Object.create(null));
        debugger;
        return obj;
    }

    get Transitions() {
        return this._transitions;
    }
    /**
     * 新建一个 Transition 
     */
    public newTransition(): Transition 
    public newTransition(trigger: string, destinationState: string): Transition 
    public newTransition(trigger?: string, destinationState?: string): Transition {
        let tr: Transition;
        if (trigger && destinationState) {
            tr = new Transition(trigger, destinationState);
        } else {
            tr = new Transition();
        }

        this._transitions.push(tr);
        this.merge();

        this.setJsonProperty("Transitions", this._transitions.map(s => s.JsonObject));
        return tr;
    }

    /**
     * 合并
     */
    private merge() {
        const obsers = this.Transitions.map(s => s.valueChange);
        this._mergeSub?.unsubscribe();
        this._mergeSub = merge(...obsers).subscribe(array => {
            this.valueChange.emit(this);
        });
    }

    remove(tr: Transition) {
        const index = this.Transitions.indexOf(tr, 0);
        if (index > -1) {
            this.Transitions.splice(index, 1);
            this.valueChange.emit(this);
        }
    }

    Permit(trigger: string, destinationState: string) {
        this.CheckDestinationState(destinationState);
        this.newTransition(trigger, destinationState);
        return this;
    }

    getTrigger(trigger: string) {
        const index = this.Transitions.findIndex(x => x.Trigger == trigger);
        if (index > -1) return this.Transitions[index];
        return null;
    }

    private CheckDestinationState(destinationState: string) {
        if (destinationState == this.State) {
            throw new Error(`目标状态不能等于原状态 ${this.State}`);
        }
    }
}