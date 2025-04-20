import { ValueChange } from "./value-change";


export interface JsonObject {
    JsonObject: { [key: string]: any };
}

export class BaseFunc<T> extends ValueChange<T> implements JsonObject {
    /**
     * 可转json的对象
     */
    JsonObject: { [key: string]: any } = {};

    /**
     * 设置一个可以被转为Json的属性、值
     * @param key 键
     * @param value 值
     */
    protected setJsonProperty(key: string, value: any) {
        this.JsonObject[key] = value;
    }
}