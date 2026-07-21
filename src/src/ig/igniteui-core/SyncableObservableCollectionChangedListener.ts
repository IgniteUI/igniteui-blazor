import { Base, Type } from "./type";

/**
 * @hidden 
 */
export interface SyncableObservableCollectionChangedListener { 
	onChanged(collection: any): void;
}

/**
 * @hidden 
 */
export let SyncableObservableCollectionChangedListener_$type = new Type(null, 'SyncableObservableCollectionChangedListener');


