import { IgcFormatSpecifier } from './igc-format-specifier';
import { FormatSpecifier } from './FormatSpecifier';
import { TypeRegistrar } from './type';

export class IgcFormatSpecifierModule {
    public static register(): void {
        
        TypeRegistrar.registerCons("IgcFormatSpecifier", IgcFormatSpecifier);
        TypeRegistrar.register("FormatSpecifier", (<any>FormatSpecifier).$type);
    }
}
