import { Base, Type } from "./type";
import { NotifyCollectionChangedEventArgs } from "./NotifyCollectionChangedEventArgs";

/**
 * @hidden 
 */
export interface INotifyCollectionChanged { 
	collectionChanged: (sender: any, e: NotifyCollectionChangedEventArgs) => void;
}

/**
 * @hidden 
 */
export let INotifyCollectionChanged_$type = new Type(null, 'INotifyCollectionChanged');


