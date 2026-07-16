import { IgcNumberFormatSpecifier } from './igc-number-format-specifier';
import { NumberFormatSpecifier } from './NumberFormatSpecifier';
import { TypeRegistrar } from './type';

export class IgcNumberFormatSpecifierModule {
    public static register(): void {
        
        TypeRegistrar.registerCons("IgcNumberFormatSpecifier", IgcNumberFormatSpecifier);
        TypeRegistrar.register("NumberFormatSpecifier", (<any>NumberFormatSpecifier).$type);
    }
}
