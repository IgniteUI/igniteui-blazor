import { IgRect } from './IgRect'
import { IgSize } from './IgSize'
import { IgPoint } from './IgPoint'
import { IgDataTemplate } from './IgDataTemplate'
import { IgcHTMLElement } from './igc-html-element'
import { FormatSpecifier as FormatSpecifier_internal } from "./FormatSpecifier";

export class IgcFormatSpecifier
{

protected createImplementation() : FormatSpecifier_internal
{
	return new FormatSpecifier_internal();
}
	protected _implementation: any;
	                            /**
	                             * @hidden 
	                             */
	public get i() : FormatSpecifier_internal {
		return this._implementation;
	}
	private onImplementationCreated() {
		
	}
	constructor() {
	this._implementation = this.createImplementation();
	(<any>this._implementation).externalObject = this;
	this.onImplementationCreated();
	                                if ((this as any)._initializeAdapters) {
	                                    (this as any)._initializeAdapters();
	                                }
	
	}
	protected _provideImplementation(i: any) {
	    this._implementation = i;
	    (<any>this._implementation).externalObject = this;
	this.onImplementationCreated();
	                                if ((this as any)._initializeAdapters) {
	                                    (this as any)._initializeAdapters();
	                                }
	
	}
	
	    public findByName(name: string): any {
	        
	    if ((this as any).findEphemera) {
	        if (name && name.indexOf("@@e:") == 0) {
	            return (this as any).findEphemera(name);
	        }
	    }
	
	
	        return null;
	    }
	public getLocalCulture() : string {
	                        
		let iv = this.i.getLocalCulture();
	                        return (iv);
	}
}
