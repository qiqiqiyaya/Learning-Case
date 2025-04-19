import { ValueChange } from "./value-change";

export class Transition extends ValueChange<Transition> {
    private _trigger: string;
    private _destinationState: string;

    get Trigger() {
        return this._trigger;
    }

    set Trigger(value: string) {
        this._trigger = value;
        this.valueChange.emit(this);
        console.log(this);
    }

    get DestinationState() {
        return this._destinationState;
    }

    set DestinationState(value: string) {
        this._destinationState = value;
        this.valueChange.emit(this);
        console.log(this);
    }

    getJson(): string {
        return JSON.stringify({ Trigger: this._trigger, DestinationState: this._destinationState });
    }
}
