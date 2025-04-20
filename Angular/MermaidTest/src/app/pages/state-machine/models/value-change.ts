import { EventEmitter } from "@angular/core";

/**
 * 对象值变更
 */
export class ValueChange<T> {
    private _valueChange: EventEmitter<T> = new EventEmitter<T>();

    get valueChange(): EventEmitter<T> {
        return this._valueChange;
    }

}