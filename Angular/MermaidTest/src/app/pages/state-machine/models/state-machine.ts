import { Subscription, combineLatest, merge } from "rxjs";
import { StateRepresentation } from "./state-representation";
import { BaseFunc } from "./base-func";


export class KeyValuePair<TKey, TValue> {
    constructor(public key: TKey, public value: TValue) { }
}
export class Dictionary<TKey extends number | string, TValue> {
    private pairs: KeyValuePair<TKey, TValue>[] = [];

    add(key: TKey, value: TValue) {
        const index = this.pairs.findIndex(x => x.key == key);
        if (index > 0) {
            throw new Error(`Duplicate key ${key}`);
        }

        this.pairs.push(new KeyValuePair<TKey, TValue>(key, value));
        this[key] = value as any;
    }

    [key: string | number]: any;
}





export class StateMachine extends BaseFunc<StateMachine> {
    private _mergeSub: Subscription;

    protected _initialState: string;
    protected _currentState: string;
    protected _stateConfiguration: StateRepresentation[] = [];

    constructor(initalState: string) {
        super();
        this.InitialState = initalState;
    }

    /**
     *  初始化状态
     */
    get InitialState(): string {
        return this._initialState;
    }
    private set InitialState(value: string) {
        this._initialState = value;
        this.setJsonProperty("InitialState", value);
        this.valueChange.emit();
    }

    /**
     *  当前状态
     */
    get CurrentState(): string {
        return this._currentState;
    }
    set CurrentState(value: string) {
        this._currentState = value;
        this.setJsonProperty("CurrentState", value);
        this.valueChange.emit();
    }

    get StateConfigurationMap(): Dictionary<string, StateRepresentation[]> {
        if (this.StateConfiguration.length == 0) return new Dictionary();

        debugger;
        const aa = this.StateConfiguration.reduce(function (r, a) {
            r[a.State] = r[a.State] || [];
            r[a.State].push(a);
            return r;
        }, Object.create(null));

        return aa;
    }

    /**
     * 状态表达配置
     */
    get StateConfiguration() {
        return this._stateConfiguration;
    }

    newStateConfiguration(state?: string): StateRepresentation {
        const sr = new StateRepresentation(state);
        this.StateConfiguration.push(sr);

        this.setJsonProperty("StateConfiguration", this.StateConfiguration.map(s => s.JsonObject));
        this.merge();
        return sr;
    }

    /**
     * 合并
     */
    private merge() {
        const obsers = this.StateConfiguration.map(s => s.valueChange);
        this._mergeSub?.unsubscribe();
        this._mergeSub = merge(...obsers).subscribe(s => {
            this.valueChange.emit(this);
        });
    }

    remove(sr: StateRepresentation) {
        const index = this.StateConfiguration.indexOf(sr, 0);
        if (index > -1) {
            this.StateConfiguration.splice(index, 1);
            this.valueChange.emit(this);
        }
    }

    /**
     * 配置
     */
    Configure(state: string): StateRepresentation {
        const sr = this.getState(state);
        if (sr) return sr;
        return this.newStateConfiguration(state);
    }

    /**
     * 是否配置过指定状态
     * @param state 状态
     * @returns 
     */
    getState(state: string): StateRepresentation | null {
        const index = this.StateConfiguration.findIndex(x => x.State == state);
        if (index > -1) {
            return this.StateConfiguration[index];
        }
        return null;
    }

    destroy() {
        this.valueChange.unsubscribe();
    }
}
