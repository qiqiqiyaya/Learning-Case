import { EventEmitter } from "@angular/core";

/**
 * 对象值变更
 */
export class ValueChange<T> {
    valueChange: EventEmitter<T> = new EventEmitter<T>();
}