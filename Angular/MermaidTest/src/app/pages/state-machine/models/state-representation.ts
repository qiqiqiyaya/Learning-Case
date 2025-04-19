import { Observable, Subscription, combineLatest, merge, mergeAll, of } from "rxjs";
import { Transition } from "./transition";
import { ValueChange } from "./value-change";

export class StateRepresentation extends ValueChange<StateRepresentation> {
    private _mergeSub: Subscription;
    private _state: string;
    private _transitions: Transition[] = [];

    // Transitions: Map<string, Transition[]> = new Map<string, Transition[]>();
    get Transitions() {
        return this._transitions;
    }

    get State() {
        return this._state;
    }
    set State(value: string) {
        this._state = value;
        this.valueChange.emit(this);
    }

    get transitionGroup(): any {
        if (this.Transitions.length == 0) return {};
        const obj= this.Transitions.reduce(function (r, a) {
            r[a.Trigger] = r[a.Trigger] || [];
            r[a.Trigger].push(a);
            return r;
        }, Object.create(null));
        debugger;
        return obj;
    }

    /**
     * 新建一个 Transition 
     */
    public newTransition(): Transition {
        const tr = new Transition();
        this._transitions.push(tr);
        this.merge();
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

    remove(tr:Transition){
        const index = this.Transitions.indexOf(tr, 0);
        if (index > -1) {
            this.Transitions.splice(index, 1);
            this.valueChange.emit(this);
        }
    }

    getJson(): string {
        

        const obj = { State: this._state, TransitionGroup: this.transitionGroup }
        return JSON.stringify(obj);
    }
}