import { BaseFunc } from "./base-func";

export class Transition extends BaseFunc<Transition> {
    private _trigger: string;
    private _destinationState: string;

    constructor()
    constructor(trigger: string, destinationState: string)
    constructor(trigger?: string, destinationState?: string) {
        super();

        if (trigger) this.Trigger = trigger;
        if (destinationState) this.DestinationState = destinationState;
    }

    get Trigger() {
        return this._trigger;
    }

    set Trigger(value: string) {
        this._trigger = value;
        this.setJsonProperty("Trigger", value);
        this.valueChange.emit(this);
    }

    get DestinationState() {
        return this._destinationState;
    }

    set DestinationState(value: string) {
        this._destinationState = value;
        this.setJsonProperty("DestinationState", value);
        this.valueChange.emit(this);
    }
}
