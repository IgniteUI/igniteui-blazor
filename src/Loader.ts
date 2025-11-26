import { TypeDescriptionContext } from 'igniteui-core/TypeDescriptionContext';
import { ComponentRenderer } from 'igniteui-core/ComponentRenderer';
import { ModuleManager } from 'igniteui-core/module-manager';
import { Base } from 'igniteui-core/type';
import { dateMinValue } from 'igniteui-core/date';
import { getAllPropertyNames } from 'igniteui-core/componentUtil';
import { TypeRegistrar } from 'igniteui-core/type';
import { Localization } from 'igniteui-core/Localization';

export interface LoadedModules {
    readonly componentModule: { register: () => void };
    readonly metadataModule: { register: (cx: TypeDescriptionContext) => void }
}
  
export class Loader {
    private static _instance: Loader = null;
    public static get instance(): Loader {
      if (Loader._instance == null) {
        Loader._instance = new Loader();
      }
      return Loader._instance;
    }
  
    public constructor() {
      
    }

    public registerResource(key: string, value: any) {
        Localization.register(key, value);
    }

    public setResourceString(grouping: string, id: string, value: string) {
        if (!id || id === "") {
            let strings = JSON.parse(value);
            if (Localization.isRegistered(grouping)) {
                let keys = Object.keys(strings);
                for (let i = 0; i < keys.length; i++) {
                    Localization.setString(grouping, keys[i], strings[keys[i]]);
                }
            } else {
                Localization.register(grouping, strings);
            }
        } else {
            Localization.setString(grouping, id, value);
        }
    }
  
    private _moduleSet: Set<string> = new Set<string>();
    private _loadingSet: Set<string> = new Set<string>();
    public async request(module: string, cr: ComponentRenderer) {
      if (this._moduleSet.has(module) ||
      this._loadingSet.has(module)) {
        return;
      }
  
      if (this.loadingPromise == null) {
        this.loadingPromise = new Promise<void>((resolve, reject) => {
          this.loadingPromiseResolve = resolve;
          this.loadingPromiseReject = reject;
        });
      }
      this._loadingSet.add(module);
      switch (module) {
    case "TemplateContainerModuleModule":
            let { IgcTemplateContainerModule } = await import('igniteui-core/igc-template-container-module');
            let { TemplateContainerDescriptionModule } = await import('igniteui-core/TemplateContainerDescriptionModule');
            this._loadingSet.delete(module);
            ModuleManager.register(
                IgcTemplateContainerModule
            );
            TemplateContainerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
    
        //@@ModuleLoading

    case "WebAccordionModule":
        {
            let { IgcAccordionComponent } = await import('igniteui-webcomponents');
            let { WebAccordionDescriptionModule } = await import('igniteui-core/WebAccordionDescriptionModule');

            this._loadingSet.delete(module);

            IgcAccordionComponent.register();
            TypeRegistrar.registerCons('IgcAccordionComponent', IgcAccordionComponent);

            WebAccordionDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebAvatarModule":
        {
            let { IgcAvatarComponent } = await import('igniteui-webcomponents');
            let { WebAvatarDescriptionModule } = await import('igniteui-core/WebAvatarDescriptionModule');

            this._loadingSet.delete(module);

            IgcAvatarComponent.register();
            TypeRegistrar.registerCons('IgcAvatarComponent', IgcAvatarComponent);

            WebAvatarDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebBadgeModule":
        {
            let { IgcBadgeComponent } = await import('igniteui-webcomponents');
            let { WebBadgeDescriptionModule } = await import('igniteui-core/WebBadgeDescriptionModule');

            this._loadingSet.delete(module);

            IgcBadgeComponent.register();
            TypeRegistrar.registerCons('IgcBadgeComponent', IgcBadgeComponent);

            WebBadgeDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebBannerModule":
        {
            let { IgcBannerComponent } = await import('igniteui-webcomponents');
            let { WebBannerDescriptionModule } = await import('igniteui-core/WebBannerDescriptionModule');

            this._loadingSet.delete(module);

            IgcBannerComponent.register();
            TypeRegistrar.registerCons('IgcBannerComponent', IgcBannerComponent);

            WebBannerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebButtonGroupModule":
        {
            let { IgcButtonGroupComponent } = await import('igniteui-webcomponents');
            let { WebButtonGroupDescriptionModule } = await import('igniteui-core/WebButtonGroupDescriptionModule');

            this._loadingSet.delete(module);

            IgcButtonGroupComponent.register();
            TypeRegistrar.registerCons('IgcButtonGroupComponent', IgcButtonGroupComponent);

            WebButtonGroupDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebButtonModule":
        {
            let { IgcButtonComponent } = await import('igniteui-webcomponents');
            let { WebButtonDescriptionModule } = await import('igniteui-core/WebButtonDescriptionModule');

            this._loadingSet.delete(module);

            IgcButtonComponent.register();
            TypeRegistrar.registerCons('IgcButtonComponent', IgcButtonComponent);

            WebButtonDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCalendarModule":
        {
            let { IgcCalendarComponent } = await import('igniteui-webcomponents');
            let { WebCalendarDescriptionModule } = await import('igniteui-core/WebCalendarDescriptionModule');

            this._loadingSet.delete(module);

            IgcCalendarComponent.register();
            TypeRegistrar.registerCons('IgcCalendarComponent', IgcCalendarComponent);

            WebCalendarDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCardActionsModule":
        {
            let { IgcCardActionsComponent } = await import('igniteui-webcomponents');
            let { WebCardActionsDescriptionModule } = await import('igniteui-core/WebCardActionsDescriptionModule');

            this._loadingSet.delete(module);

            IgcCardActionsComponent.register();
            TypeRegistrar.registerCons('IgcCardActionsComponent', IgcCardActionsComponent);

            WebCardActionsDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCardContentModule":
        {
            let { IgcCardContentComponent } = await import('igniteui-webcomponents');
            let { WebCardContentDescriptionModule } = await import('igniteui-core/WebCardContentDescriptionModule');

            this._loadingSet.delete(module);

            IgcCardContentComponent.register();
            TypeRegistrar.registerCons('IgcCardContentComponent', IgcCardContentComponent);

            WebCardContentDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCardHeaderModule":
        {
            let { IgcCardHeaderComponent } = await import('igniteui-webcomponents');
            let { WebCardHeaderDescriptionModule } = await import('igniteui-core/WebCardHeaderDescriptionModule');

            this._loadingSet.delete(module);

            IgcCardHeaderComponent.register();
            TypeRegistrar.registerCons('IgcCardHeaderComponent', IgcCardHeaderComponent);

            WebCardHeaderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCardMediaModule":
        {
            let { IgcCardMediaComponent } = await import('igniteui-webcomponents');
            let { WebCardMediaDescriptionModule } = await import('igniteui-core/WebCardMediaDescriptionModule');

            this._loadingSet.delete(module);

            IgcCardMediaComponent.register();
            TypeRegistrar.registerCons('IgcCardMediaComponent', IgcCardMediaComponent);

            WebCardMediaDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCardModule":
        {
            let { IgcCardComponent } = await import('igniteui-webcomponents');
            let { WebCardDescriptionModule } = await import('igniteui-core/WebCardDescriptionModule');

            this._loadingSet.delete(module);

            IgcCardComponent.register();
            TypeRegistrar.registerCons('IgcCardComponent', IgcCardComponent);

            WebCardDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCarouselIndicatorModule":
        {
            let { IgcCarouselIndicatorComponent } = await import('igniteui-webcomponents');
            let { WebCarouselIndicatorDescriptionModule } = await import('igniteui-core/WebCarouselIndicatorDescriptionModule');

            this._loadingSet.delete(module);

            IgcCarouselIndicatorComponent.register();
            TypeRegistrar.registerCons('IgcCarouselIndicatorComponent', IgcCarouselIndicatorComponent);

            WebCarouselIndicatorDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCarouselModule":
        {
            let { IgcCarouselComponent } = await import('igniteui-webcomponents');
            let { WebCarouselDescriptionModule } = await import('igniteui-core/WebCarouselDescriptionModule');

            this._loadingSet.delete(module);

            IgcCarouselComponent.register();
            TypeRegistrar.registerCons('IgcCarouselComponent', IgcCarouselComponent);

            WebCarouselDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCarouselSlideModule":
        {
            let { IgcCarouselSlideComponent } = await import('igniteui-webcomponents');
            let { WebCarouselSlideDescriptionModule } = await import('igniteui-core/WebCarouselSlideDescriptionModule');

            this._loadingSet.delete(module);

            IgcCarouselSlideComponent.register();
            TypeRegistrar.registerCons('IgcCarouselSlideComponent', IgcCarouselSlideComponent);

            WebCarouselSlideDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCheckboxModule":
        {
            let { IgcCheckboxComponent } = await import('igniteui-webcomponents');
            let { WebCheckboxDescriptionModule } = await import('igniteui-core/WebCheckboxDescriptionModule');

            this._loadingSet.delete(module);

            IgcCheckboxComponent.register();
            TypeRegistrar.registerCons('IgcCheckboxComponent', IgcCheckboxComponent);

            WebCheckboxDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebChipModule":
        {
            let { IgcChipComponent } = await import('igniteui-webcomponents');
            let { WebChipDescriptionModule } = await import('igniteui-core/WebChipDescriptionModule');

            this._loadingSet.delete(module);

            IgcChipComponent.register();
            TypeRegistrar.registerCons('IgcChipComponent', IgcChipComponent);

            WebChipDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCircularGradientModule":
        {
            let { IgcCircularGradientComponent } = await import('igniteui-webcomponents');
            let { WebCircularGradientDescriptionModule } = await import('igniteui-core/WebCircularGradientDescriptionModule');

            this._loadingSet.delete(module);

            IgcCircularGradientComponent.register();
            TypeRegistrar.registerCons('IgcCircularGradientComponent', IgcCircularGradientComponent);

            WebCircularGradientDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebCircularProgressModule":
        {
            let { IgcCircularProgressComponent } = await import('igniteui-webcomponents');
            let { WebCircularProgressDescriptionModule } = await import('igniteui-core/WebCircularProgressDescriptionModule');

            this._loadingSet.delete(module);

            IgcCircularProgressComponent.register();
            TypeRegistrar.registerCons('IgcCircularProgressComponent', IgcCircularProgressComponent);

            WebCircularProgressDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebComboModule":
        {
            let { IgcComboComponent } = await import('igniteui-webcomponents');
            let { WebComboDescriptionModule } = await import('igniteui-core/WebComboDescriptionModule');

            this._loadingSet.delete(module);

            IgcComboComponent.register();
            TypeRegistrar.registerCons('IgcComboComponent', IgcComboComponent);

            WebComboDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDatePickerModule":
        {
            let { IgcDatePickerComponent } = await import('igniteui-webcomponents');
            let { WebDatePickerDescriptionModule } = await import('igniteui-core/WebDatePickerDescriptionModule');

            this._loadingSet.delete(module);

            IgcDatePickerComponent.register();
            TypeRegistrar.registerCons('IgcDatePickerComponent', IgcDatePickerComponent);

            WebDatePickerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDateRangePickerModule":
        {
            let { IgcDateRangePickerComponent } = await import('igniteui-webcomponents');
            let { WebDateRangePickerDescriptionModule } = await import('igniteui-core/WebDateRangePickerDescriptionModule');

            this._loadingSet.delete(module);

            IgcDateRangePickerComponent.register();
            TypeRegistrar.registerCons('IgcDateRangePickerComponent', IgcDateRangePickerComponent);

            WebDateRangePickerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDateTimeInputModule":
        {
            let { IgcDateTimeInputComponent } = await import('igniteui-webcomponents');
            let { WebDateTimeInputDescriptionModule } = await import('igniteui-core/WebDateTimeInputDescriptionModule');

            this._loadingSet.delete(module);

            IgcDateTimeInputComponent.register();
            TypeRegistrar.registerCons('IgcDateTimeInputComponent', IgcDateTimeInputComponent);

            WebDateTimeInputDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDialogModule":
        {
            let { IgcDialogComponent } = await import('igniteui-webcomponents');
            let { WebDialogDescriptionModule } = await import('igniteui-core/WebDialogDescriptionModule');

            this._loadingSet.delete(module);

            IgcDialogComponent.register();
            TypeRegistrar.registerCons('IgcDialogComponent', IgcDialogComponent);

            WebDialogDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDividerModule":
        {
            let { IgcDividerComponent } = await import('igniteui-webcomponents');
            let { WebDividerDescriptionModule } = await import('igniteui-core/WebDividerDescriptionModule');

            this._loadingSet.delete(module);

            IgcDividerComponent.register();
            TypeRegistrar.registerCons('IgcDividerComponent', IgcDividerComponent);

            WebDividerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDropdownGroupModule":
        {
            let { IgcDropdownGroupComponent } = await import('igniteui-webcomponents');
            let { WebDropdownGroupDescriptionModule } = await import('igniteui-core/WebDropdownGroupDescriptionModule');

            this._loadingSet.delete(module);

            IgcDropdownGroupComponent.register();
            TypeRegistrar.registerCons('IgcDropdownGroupComponent', IgcDropdownGroupComponent);

            WebDropdownGroupDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDropdownHeaderModule":
        {
            let { IgcDropdownHeaderComponent } = await import('igniteui-webcomponents');
            let { WebDropdownHeaderDescriptionModule } = await import('igniteui-core/WebDropdownHeaderDescriptionModule');

            this._loadingSet.delete(module);

            IgcDropdownHeaderComponent.register();
            TypeRegistrar.registerCons('IgcDropdownHeaderComponent', IgcDropdownHeaderComponent);

            WebDropdownHeaderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDropdownItemModule":
        {
            let { IgcDropdownItemComponent } = await import('igniteui-webcomponents');
            let { WebDropdownItemDescriptionModule } = await import('igniteui-core/WebDropdownItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcDropdownItemComponent.register();
            TypeRegistrar.registerCons('IgcDropdownItemComponent', IgcDropdownItemComponent);

            WebDropdownItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebDropdownModule":
        {
            let { IgcDropdownComponent } = await import('igniteui-webcomponents');
            let { WebDropdownDescriptionModule } = await import('igniteui-core/WebDropdownDescriptionModule');

            this._loadingSet.delete(module);

            IgcDropdownComponent.register();
            TypeRegistrar.registerCons('IgcDropdownComponent', IgcDropdownComponent);

            WebDropdownDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebExpansionPanelModule":
        {
            let { IgcExpansionPanelComponent } = await import('igniteui-webcomponents');
            let { WebExpansionPanelDescriptionModule } = await import('igniteui-core/WebExpansionPanelDescriptionModule');

            this._loadingSet.delete(module);

            IgcExpansionPanelComponent.register();
            TypeRegistrar.registerCons('IgcExpansionPanelComponent', IgcExpansionPanelComponent);

            WebExpansionPanelDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "FormatSpecifierModule":
        let { IgcFormatSpecifierModule } = await import('igniteui-core/igc-format-specifier-module');
        let { FormatSpecifierDescriptionModule } = await import('igniteui-core/FormatSpecifierDescriptionModule');
        this._loadingSet.delete(module);
        ModuleManager.register(
        IgcFormatSpecifierModule
        );
        FormatSpecifierDescriptionModule.register(cr.context);
        this.checkDone();
        break;
            
    case "WebIconButtonModule":
        {
            let { IgcIconButtonComponent } = await import('igniteui-webcomponents');
            let { WebIconButtonDescriptionModule } = await import('igniteui-core/WebIconButtonDescriptionModule');

            this._loadingSet.delete(module);

            IgcIconButtonComponent.register();
            TypeRegistrar.registerCons('IgcIconButtonComponent', IgcIconButtonComponent);

            WebIconButtonDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebIconModule":
        {
            let { IgcIconComponent } = await import('igniteui-webcomponents');
            let { WebIconDescriptionModule } = await import('igniteui-core/WebIconDescriptionModule');

            this._loadingSet.delete(module);

            IgcIconComponent.register();
            TypeRegistrar.registerCons('IgcIconComponent', IgcIconComponent);

            WebIconDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebInputModule":
        {
            let { IgcInputComponent } = await import('igniteui-webcomponents');
            let { WebInputDescriptionModule } = await import('igniteui-core/WebInputDescriptionModule');

            this._loadingSet.delete(module);

            IgcInputComponent.register();
            TypeRegistrar.registerCons('IgcInputComponent', IgcInputComponent);

            WebInputDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebLinearProgressModule":
        {
            let { IgcLinearProgressComponent } = await import('igniteui-webcomponents');
            let { WebLinearProgressDescriptionModule } = await import('igniteui-core/WebLinearProgressDescriptionModule');

            this._loadingSet.delete(module);

            IgcLinearProgressComponent.register();
            TypeRegistrar.registerCons('IgcLinearProgressComponent', IgcLinearProgressComponent);

            WebLinearProgressDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebListHeaderModule":
        {
            let { IgcListHeaderComponent } = await import('igniteui-webcomponents');
            let { WebListHeaderDescriptionModule } = await import('igniteui-core/WebListHeaderDescriptionModule');

            this._loadingSet.delete(module);

            IgcListHeaderComponent.register();
            TypeRegistrar.registerCons('IgcListHeaderComponent', IgcListHeaderComponent);

            WebListHeaderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebListItemModule":
        {
            let { IgcListItemComponent } = await import('igniteui-webcomponents');
            let { WebListItemDescriptionModule } = await import('igniteui-core/WebListItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcListItemComponent.register();
            TypeRegistrar.registerCons('IgcListItemComponent', IgcListItemComponent);

            WebListItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebListModule":
        {
            let { IgcListComponent } = await import('igniteui-webcomponents');
            let { WebListDescriptionModule } = await import('igniteui-core/WebListDescriptionModule');

            this._loadingSet.delete(module);

            IgcListComponent.register();
            TypeRegistrar.registerCons('IgcListComponent', IgcListComponent);

            WebListDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebMaskInputModule":
        {
            let { IgcMaskInputComponent } = await import('igniteui-webcomponents');
            let { WebMaskInputDescriptionModule } = await import('igniteui-core/WebMaskInputDescriptionModule');

            this._loadingSet.delete(module);

            IgcMaskInputComponent.register();
            TypeRegistrar.registerCons('IgcMaskInputComponent', IgcMaskInputComponent);

            WebMaskInputDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebNavDrawerHeaderItemModule":
        {
            let { IgcNavDrawerHeaderItemComponent } = await import('igniteui-webcomponents');
            let { WebNavDrawerHeaderItemDescriptionModule } = await import('igniteui-core/WebNavDrawerHeaderItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcNavDrawerHeaderItemComponent.register();
            TypeRegistrar.registerCons('IgcNavDrawerHeaderItemComponent', IgcNavDrawerHeaderItemComponent);

            WebNavDrawerHeaderItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebNavDrawerItemModule":
        {
            let { IgcNavDrawerItemComponent } = await import('igniteui-webcomponents');
            let { WebNavDrawerItemDescriptionModule } = await import('igniteui-core/WebNavDrawerItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcNavDrawerItemComponent.register();
            TypeRegistrar.registerCons('IgcNavDrawerItemComponent', IgcNavDrawerItemComponent);

            WebNavDrawerItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebNavDrawerModule":
        {
            let { IgcNavDrawerComponent } = await import('igniteui-webcomponents');
            let { WebNavDrawerDescriptionModule } = await import('igniteui-core/WebNavDrawerDescriptionModule');

            this._loadingSet.delete(module);

            IgcNavDrawerComponent.register();
            TypeRegistrar.registerCons('IgcNavDrawerComponent', IgcNavDrawerComponent);

            WebNavDrawerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebNavbarModule":
        {
            let { IgcNavbarComponent } = await import('igniteui-webcomponents');
            let { WebNavbarDescriptionModule } = await import('igniteui-core/WebNavbarDescriptionModule');

            this._loadingSet.delete(module);

            IgcNavbarComponent.register();
            TypeRegistrar.registerCons('IgcNavbarComponent', IgcNavbarComponent);

            WebNavbarDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "NumberFormatSpecifierModule":
        let { IgcNumberFormatSpecifierModule } = await import('igniteui-core/igc-number-format-specifier-module');
        let { NumberFormatSpecifierDescriptionModule } = await import('igniteui-core/NumberFormatSpecifierDescriptionModule');
        this._loadingSet.delete(module);
        ModuleManager.register(
        IgcNumberFormatSpecifierModule
        );
        NumberFormatSpecifierDescriptionModule.register(cr.context);
        this.checkDone();
        break;
            
    case "WebRadioGroupModule":
        {
            let { IgcRadioGroupComponent } = await import('igniteui-webcomponents');
            let { WebRadioGroupDescriptionModule } = await import('igniteui-core/WebRadioGroupDescriptionModule');

            this._loadingSet.delete(module);

            IgcRadioGroupComponent.register();
            TypeRegistrar.registerCons('IgcRadioGroupComponent', IgcRadioGroupComponent);

            WebRadioGroupDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebRadioModule":
        {
            let { IgcRadioComponent } = await import('igniteui-webcomponents');
            let { WebRadioDescriptionModule } = await import('igniteui-core/WebRadioDescriptionModule');

            this._loadingSet.delete(module);

            IgcRadioComponent.register();
            TypeRegistrar.registerCons('IgcRadioComponent', IgcRadioComponent);

            WebRadioDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebRangeSliderModule":
        {
            let { IgcRangeSliderComponent } = await import('igniteui-webcomponents');
            let { WebRangeSliderDescriptionModule } = await import('igniteui-core/WebRangeSliderDescriptionModule');

            this._loadingSet.delete(module);

            IgcRangeSliderComponent.register();
            TypeRegistrar.registerCons('IgcRangeSliderComponent', IgcRangeSliderComponent);

            WebRangeSliderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebRatingModule":
        {
            let { IgcRatingComponent } = await import('igniteui-webcomponents');
            let { WebRatingDescriptionModule } = await import('igniteui-core/WebRatingDescriptionModule');

            this._loadingSet.delete(module);

            IgcRatingComponent.register();
            TypeRegistrar.registerCons('IgcRatingComponent', IgcRatingComponent);

            WebRatingDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebRatingSymbolModule":
        {
            let { IgcRatingSymbolComponent } = await import('igniteui-webcomponents');
            let { WebRatingSymbolDescriptionModule } = await import('igniteui-core/WebRatingSymbolDescriptionModule');

            this._loadingSet.delete(module);

            IgcRatingSymbolComponent.register();
            TypeRegistrar.registerCons('IgcRatingSymbolComponent', IgcRatingSymbolComponent);

            WebRatingSymbolDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebRippleModule":
        {
            let { IgcRippleComponent } = await import('igniteui-webcomponents');
            let { WebRippleDescriptionModule } = await import('igniteui-core/WebRippleDescriptionModule');

            this._loadingSet.delete(module);

            IgcRippleComponent.register();
            TypeRegistrar.registerCons('IgcRippleComponent', IgcRippleComponent);

            WebRippleDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSelectGroupModule":
        {
            let { IgcSelectGroupComponent } = await import('igniteui-webcomponents');
            let { WebSelectGroupDescriptionModule } = await import('igniteui-core/WebSelectGroupDescriptionModule');

            this._loadingSet.delete(module);

            IgcSelectGroupComponent.register();
            TypeRegistrar.registerCons('IgcSelectGroupComponent', IgcSelectGroupComponent);

            WebSelectGroupDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSelectHeaderModule":
        {
            let { IgcSelectHeaderComponent } = await import('igniteui-webcomponents');
            let { WebSelectHeaderDescriptionModule } = await import('igniteui-core/WebSelectHeaderDescriptionModule');

            this._loadingSet.delete(module);

            IgcSelectHeaderComponent.register();
            TypeRegistrar.registerCons('IgcSelectHeaderComponent', IgcSelectHeaderComponent);

            WebSelectHeaderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSelectItemModule":
        {
            let { IgcSelectItemComponent } = await import('igniteui-webcomponents');
            let { WebSelectItemDescriptionModule } = await import('igniteui-core/WebSelectItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcSelectItemComponent.register();
            TypeRegistrar.registerCons('IgcSelectItemComponent', IgcSelectItemComponent);

            WebSelectItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSelectModule":
        {
            let { IgcSelectComponent } = await import('igniteui-webcomponents');
            let { WebSelectDescriptionModule } = await import('igniteui-core/WebSelectDescriptionModule');

            this._loadingSet.delete(module);

            IgcSelectComponent.register();
            TypeRegistrar.registerCons('IgcSelectComponent', IgcSelectComponent);

            WebSelectDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSliderLabelModule":
        {
            let { IgcSliderLabelComponent } = await import('igniteui-webcomponents');
            let { WebSliderLabelDescriptionModule } = await import('igniteui-core/WebSliderLabelDescriptionModule');

            this._loadingSet.delete(module);

            IgcSliderLabelComponent.register();
            TypeRegistrar.registerCons('IgcSliderLabelComponent', IgcSliderLabelComponent);

            WebSliderLabelDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSliderModule":
        {
            let { IgcSliderComponent } = await import('igniteui-webcomponents');
            let { WebSliderDescriptionModule } = await import('igniteui-core/WebSliderDescriptionModule');

            this._loadingSet.delete(module);

            IgcSliderComponent.register();
            TypeRegistrar.registerCons('IgcSliderComponent', IgcSliderComponent);

            WebSliderDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSnackbarModule":
        {
            let { IgcSnackbarComponent } = await import('igniteui-webcomponents');
            let { WebSnackbarDescriptionModule } = await import('igniteui-core/WebSnackbarDescriptionModule');

            this._loadingSet.delete(module);

            IgcSnackbarComponent.register();
            TypeRegistrar.registerCons('IgcSnackbarComponent', IgcSnackbarComponent);

            WebSnackbarDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebStepModule":
        {
            let { IgcStepComponent } = await import('igniteui-webcomponents');
            let { WebStepDescriptionModule } = await import('igniteui-core/WebStepDescriptionModule');

            this._loadingSet.delete(module);

            IgcStepComponent.register();
            TypeRegistrar.registerCons('IgcStepComponent', IgcStepComponent);

            WebStepDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebStepperModule":
        {
            let { IgcStepperComponent } = await import('igniteui-webcomponents');
            let { WebStepperDescriptionModule } = await import('igniteui-core/WebStepperDescriptionModule');

            this._loadingSet.delete(module);

            IgcStepperComponent.register();
            TypeRegistrar.registerCons('IgcStepperComponent', IgcStepperComponent);

            WebStepperDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebSwitchModule":
        {
            let { IgcSwitchComponent } = await import('igniteui-webcomponents');
            let { WebSwitchDescriptionModule } = await import('igniteui-core/WebSwitchDescriptionModule');

            this._loadingSet.delete(module);

            IgcSwitchComponent.register();
            TypeRegistrar.registerCons('IgcSwitchComponent', IgcSwitchComponent);

            WebSwitchDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTabModule":
        {
            let { IgcTabComponent } = await import('igniteui-webcomponents');
            let { WebTabDescriptionModule } = await import('igniteui-core/WebTabDescriptionModule');

            this._loadingSet.delete(module);

            IgcTabComponent.register();
            TypeRegistrar.registerCons('IgcTabComponent', IgcTabComponent);

            WebTabDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTabsModule":
        {
            let { IgcTabsComponent } = await import('igniteui-webcomponents');
            let { WebTabsDescriptionModule } = await import('igniteui-core/WebTabsDescriptionModule');

            this._loadingSet.delete(module);

            IgcTabsComponent.register();
            TypeRegistrar.registerCons('IgcTabsComponent', IgcTabsComponent);

            WebTabsDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTextareaModule":
        {
            let { IgcTextareaComponent } = await import('igniteui-webcomponents');
            let { WebTextareaDescriptionModule } = await import('igniteui-core/WebTextareaDescriptionModule');

            this._loadingSet.delete(module);

            IgcTextareaComponent.register();
            TypeRegistrar.registerCons('IgcTextareaComponent', IgcTextareaComponent);

            WebTextareaDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTileManagerModule":
        {
            let { IgcTileManagerComponent } = await import('igniteui-webcomponents');
            let { WebTileManagerDescriptionModule } = await import('igniteui-core/WebTileManagerDescriptionModule');

            this._loadingSet.delete(module);

            IgcTileManagerComponent.register();
            TypeRegistrar.registerCons('IgcTileManagerComponent', IgcTileManagerComponent);

            WebTileManagerDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTileModule":
        {
            let { IgcTileComponent } = await import('igniteui-webcomponents');
            let { WebTileDescriptionModule } = await import('igniteui-core/WebTileDescriptionModule');

            this._loadingSet.delete(module);

            IgcTileComponent.register();
            TypeRegistrar.registerCons('IgcTileComponent', IgcTileComponent);

            WebTileDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebToastModule":
        {
            let { IgcToastComponent } = await import('igniteui-webcomponents');
            let { WebToastDescriptionModule } = await import('igniteui-core/WebToastDescriptionModule');

            this._loadingSet.delete(module);

            IgcToastComponent.register();
            TypeRegistrar.registerCons('IgcToastComponent', IgcToastComponent);

            WebToastDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebToggleButtonModule":
        {
            let { IgcToggleButtonComponent } = await import('igniteui-webcomponents');
            let { WebToggleButtonDescriptionModule } = await import('igniteui-core/WebToggleButtonDescriptionModule');

            this._loadingSet.delete(module);

            IgcToggleButtonComponent.register();
            TypeRegistrar.registerCons('IgcToggleButtonComponent', IgcToggleButtonComponent);

            WebToggleButtonDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTooltipModule":
        {
            let { IgcTooltipComponent } = await import('igniteui-webcomponents');
            let { WebTooltipDescriptionModule } = await import('igniteui-core/WebTooltipDescriptionModule');

            this._loadingSet.delete(module);

            IgcTooltipComponent.register();
            TypeRegistrar.registerCons('IgcTooltipComponent', IgcTooltipComponent);

            WebTooltipDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTreeItemModule":
        {
            let { IgcTreeItemComponent } = await import('igniteui-webcomponents');
            let { WebTreeItemDescriptionModule } = await import('igniteui-core/WebTreeItemDescriptionModule');

            this._loadingSet.delete(module);

            IgcTreeItemComponent.register();
            TypeRegistrar.registerCons('IgcTreeItemComponent', IgcTreeItemComponent);

            WebTreeItemDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
    case "WebTreeModule":
        {
            let { IgcTreeComponent } = await import('igniteui-webcomponents');
            let { WebTreeDescriptionModule } = await import('igniteui-core/WebTreeDescriptionModule');

            this._loadingSet.delete(module);

            IgcTreeComponent.register();
            TypeRegistrar.registerCons('IgcTreeComponent', IgcTreeComponent);

            WebTreeDescriptionModule.register(cr.context);
            this.checkDone();
            break;
        }
            
//@@ModuleLoadingEnd
      }
    }

    private static marshalIdByValueOnceSet: Map<string, string> = new Map<string, string>();
    public static marshalIdByValueOnce(id: string, event: string) {
        if (!Loader.marshalIdByValueOnceSet.has(id)) {
            Loader.marshalIdByValueOnceSet.set(id, event);
        }
    }

    public static isMarshalIdByValueOnce(id: string): boolean {
        return Loader.marshalIdByValueOnceSet.has(id);
    }

    public static clearMarshalIdByValueOnceByEvent(event: string) {
        if (event) {
            Loader.marshalIdByValueOnceSet.forEach((v, k, m) => {
                if (v === event) {
                    Loader.marshalIdByValueOnceSet.delete(k);
                }
            });
            return;
        }
        Loader.marshalIdByValueOnceSet.clear();
    }

    private static marshalByValueSet: Set<string> = null;
    public static isMarshalByValue(val: any) {
        if (val == null || val == undefined) {
            return false;
        }
        // if (val.___mustPassByValue) {
        //     return true;
        // }

        if (val._implementation && val._implementation.___mustPassByValue) {
            return true;
        }

        var name: string = null;

        if (val.$type) {
            name = val.$type.name;
        }
        
        if (val._implementation && val._implementation.$type) {
            //sometimes we seem to slap a $type tag on a public type, and it blocks
            //traversal to the implementation name here.
            name = val._implementation.$type.name;
        }

        if (!name) {
            return false;
        }

        if (Loader.marshalByValueSet == null) {
            Loader.marshalByValueSet = new Set<string>();
//@@MarshalByValue
Loader.marshalByValueSet.add('CalendarDate');
Loader.marshalByValueSet.add('CalendarFormatOptions');
Loader.marshalByValueSet.add('FocusOptions');
Loader.marshalByValueSet.add('FormatSpecifier');
Loader.marshalByValueSet.add('NumberFormatSpecifier');
Loader.marshalByValueSet.add('ActiveStepChangedEventArgs');
Loader.marshalByValueSet.add('WebActiveStepChangedEventArgs');
Loader.marshalByValueSet.add('ActiveStepChangedEventArgsDetail');
Loader.marshalByValueSet.add('WebActiveStepChangedEventArgsDetail');
Loader.marshalByValueSet.add('ActiveStepChangingEventArgs');
Loader.marshalByValueSet.add('WebActiveStepChangingEventArgs');
Loader.marshalByValueSet.add('ActiveStepChangingEventArgsDetail');
Loader.marshalByValueSet.add('WebActiveStepChangingEventArgsDetail');
Loader.marshalByValueSet.add('CheckboxChangeEventArgs');
Loader.marshalByValueSet.add('WebCheckboxChangeEventArgs');
Loader.marshalByValueSet.add('CheckboxChangeEventArgsDetail');
Loader.marshalByValueSet.add('WebCheckboxChangeEventArgsDetail');
Loader.marshalByValueSet.add('ComboChangeEventArgs');
Loader.marshalByValueSet.add('WebComboChangeEventArgs');
Loader.marshalByValueSet.add('ComboChangeEventArgsDetail');
Loader.marshalByValueSet.add('WebComboChangeEventArgsDetail');
Loader.marshalByValueSet.add('ComponentBoolValueChangedEventArgs');
Loader.marshalByValueSet.add('WebComponentBoolValueChangedEventArgs');
Loader.marshalByValueSet.add('ComponentDateValueChangedEventArgs');
Loader.marshalByValueSet.add('WebComponentDateValueChangedEventArgs');
Loader.marshalByValueSet.add('ComponentValueChangedEventArgs');
Loader.marshalByValueSet.add('WebComponentValueChangedEventArgs');
Loader.marshalByValueSet.add('DateRangeValueDetail');
Loader.marshalByValueSet.add('WebDateRangeValueDetail');
Loader.marshalByValueSet.add('DateRangeValueEventArgs');
Loader.marshalByValueSet.add('WebDateRangeValueEventArgs');
Loader.marshalByValueSet.add('DropdownItemComponentEventArgs');
Loader.marshalByValueSet.add('WebDropdownItemComponentEventArgs');
Loader.marshalByValueSet.add('ExpansionPanelComponentEventArgs');
Loader.marshalByValueSet.add('WebExpansionPanelComponentEventArgs');
Loader.marshalByValueSet.add('IconMeta');
Loader.marshalByValueSet.add('WebIconMeta');
Loader.marshalByValueSet.add('NumberEventArgs');
Loader.marshalByValueSet.add('WebNumberEventArgs');
Loader.marshalByValueSet.add('RadioChangeEventArgs');
Loader.marshalByValueSet.add('WebRadioChangeEventArgs');
Loader.marshalByValueSet.add('RadioChangeEventArgsDetail');
Loader.marshalByValueSet.add('WebRadioChangeEventArgsDetail');
Loader.marshalByValueSet.add('RangeSliderValue');
Loader.marshalByValueSet.add('WebRangeSliderValue');
Loader.marshalByValueSet.add('SelectItemComponentEventArgs');
Loader.marshalByValueSet.add('WebSelectItemComponentEventArgs');
Loader.marshalByValueSet.add('TabComponentEventArgs');
Loader.marshalByValueSet.add('WebTabComponentEventArgs');
Loader.marshalByValueSet.add('TabHeaderElement');
Loader.marshalByValueSet.add('WebTabHeaderElement');
Loader.marshalByValueSet.add('TileChangeStateEventArgs');
Loader.marshalByValueSet.add('WebTileChangeStateEventArgs');
Loader.marshalByValueSet.add('TileChangeStateEventArgsDetail');
Loader.marshalByValueSet.add('WebTileChangeStateEventArgsDetail');
Loader.marshalByValueSet.add('TileComponentEventArgs');
Loader.marshalByValueSet.add('WebTileComponentEventArgs');
Loader.marshalByValueSet.add('TreeItemComponentEventArgs');
Loader.marshalByValueSet.add('WebTreeItemComponentEventArgs');
Loader.marshalByValueSet.add('TreeSelectionEventArgs');
Loader.marshalByValueSet.add('WebTreeSelectionEventArgs');
Loader.marshalByValueSet.add('TreeSelectionEventArgsDetail');
Loader.marshalByValueSet.add('WebTreeSelectionEventArgsDetail');

//@@MarshalByValueEnd
        }

        return Loader.marshalByValueSet.has(name);
    }

    private static getContainerIgIdAttribute(container: HTMLElement) {
        const igKey = container.getAttribute(Loader.DATA_IG_ID_ATTRIBUTE);
        if (!igKey) {
            return null;
        }
        if (igKey.trim().length === 0) {
            return null;
        }
        return igKey;
    }

    private static hasContainerIgIdAttribute(container: any) {
        return container && container.hasAttribute && container.hasAttribute(Loader.DATA_IG_ID_ATTRIBUTE);
    }

    private static DATA_IG_ID_ATTRIBUTE: string = "data-ig-id";

    public static hasId(obj: any) {
        return obj.id ||
        obj.name ||
        (obj.tagName && Loader.hasContainerIgIdAttribute(obj));
    }

    public static getId(obj: any) {
        if (obj.name) {
            return obj.name;
        }
        if (obj.tagName && Loader.hasContainerIgIdAttribute) {
            return Loader.getContainerIgIdAttribute(obj);
        }
        if (obj.id) {
            return obj.id;
        }
        return Loader.getContainerIgIdAttribute(obj);
    }

    public static transformReturn(retVal: any, root: any, owner: any, key: string) {
        if (typeof(retVal) != "object") {
            if (key && owner[key] && owner[key] instanceof Date) {
                // TFS 273070 - Browsers don't seem to like older dates and will use weird timezone offsets for them. These dates
                // are timezone dependent, for example in North American timezones dates before November 1883 will have weird offsets
                // where as the UK has a cutoff date at 1847.  That is a really hard problem to solve and most people don't seem to
                // be using old dates so for now we will just check if the date is a min date and then return the appropriate ISO string
                // for it so .NET can parse it correctly.
                var minDate = dateMinValue();
                if (owner[key].getTime() === minDate.getTime()) {
                    function formatTens(num: number): string {
                        return num < 10 ? "0" + num.toString() : num.toString();
                    }
                    function formatThousands(num: number): string {
                        if (num < 10) { return "000" + num; }
                        if (num < 100) { return "00" + num; }
                        if (num < 1000) { return "0" + num; }
                        return num.toString();
                    }
                    retVal =
                        formatThousands(minDate.getFullYear()) + "-" +
                        formatTens(minDate.getMonth() + 1) + "-" +
                        formatTens(minDate.getDate()) + "T00:00:00.000";
                }
                retVal = {
                    __bounce: true,
                    retType: "date",
                    value: retVal
                };
            }
            return retVal;
        }

        var name = "";
        if (retVal && retVal._implementation) {
            if (retVal._implementation.$type) {
                name = retVal._implementation.$type.name;
            }
        } else if (retVal && retVal.$type) {
            name = retVal.$type.name;
        }

        if (retVal && retVal.___type) {
            name = retVal.___type;
        }

        if (retVal && retVal.createImplementation) {
            name = retVal.createImplementation()?.$type?.name;
        }

        if (retVal instanceof FormData) {
            let keys = [];
            let values = [];
            retVal.forEach(function(value, key){
                keys.push(key ? key : null);
                values.push(value);
            });
            
            retVal = {
                __bounce: true,
                retType: "object",
                type: "FormData",
                value: {
                    __bounce: true,
                    ___cloned: true,
                    keys: keys,
                    values: values
                }
            }
            return retVal;
        }

        var hasName = Loader.hasName(retVal, root);
        var mustPassByValue = false;
        if (retVal && retVal._implementation && retVal._implementation.___mustPassByValue) {
            mustPassByValue = true;
        }
        
        if (Loader.isMarshalByValue(retVal) && (!hasName || mustPassByValue)) {
            
            // Mainly added for autogen'd columns in the grid but other elements may need this later. When columns are autogen'd they are
            // marked as mustPassByValue so Blazor can get a copy of it. However this should only be done once so I've added
            // a mustPassByValueOnce flag which will flip mustPassByValue to false. This should make the object pass by reference
            // subsequent times when it needs to be marshalled across.
            if (retVal._implementation && retVal._implementation.___mustPassByValueOnce) {
                if (retVal._implementation.___mustPassByValue) {
                    retVal._implementation.___mustPassByValue = false;
                }
            }

            retVal = {
                __bounce: true,
                retType: Array.isArray(retVal) ? "Array" : typeof(retVal),
                type: name,
                value: retVal
            }
        } else {
            if (retVal && retVal.___id && !Loader.isMarshalIdByValueOnce(retVal.___id)) {
                retVal = { refType: "uuid", id: retVal.___id };
            } else if (hasName) {
                retVal = { refType: "name", id: Loader.getName(retVal, root) };
            } else if (retVal && retVal.___localJson) {
                retVal = { __bounce: true, retType: "localJson", value: retVal };
            } else {
                retVal = {
                __bounce: true,
                retType: Array.isArray(retVal) ? "Array" : typeof(retVal),
                type: name,
                value: retVal
                }
            }
        }
        return retVal;
    }

    private static isRoot(value: any, root: any) {
        if (value === root) {
            return true;
        }
        if (value.i &&
            value.i.nativeElement) {
            var nat = value.i.nativeElement;
            if (nat.___wcElement) {
                nat = nat.___wcElement;
            }
            if (nat == root) {
                return true;
            }
        }
        return false;
    }

    private static getName(value: any, root: any) {
        if (!value) {
            return null;
        }
        if (value && (value._styling || value._implementation) && value.name) {
            return value.name;
        }
        if (Loader.isRoot(value, root)) {
            return "mainControl";
        }
        if (value && !value._implementation && (Loader.hasId(value)) && value.tagName && value.tagName.toLowerCase().indexOf("igc-") == 0) {
            return Loader.getId(value);
        }
        if (value && value.i && value.i.nativeElement) {
            var nat = value.i.nativeElement;
            if (nat.___wcElement) {
                nat = nat.___wcElement;
            }
            return Loader.getName(nat,root);
        }
        return null;
    }

    private static hasName(value: any, root: any) {
        if (!value) {
            return false;
        }
        var hasName = false;
        if (value && (value._styling || value._implementation) && value.name) {
            hasName = true;
            return hasName;
        }
        if (Loader.isRoot(value, root)) {
            hasName = true;
        }
        if (value && !value._implementation && (Loader.hasId(value)) && value.tagName && value.tagName.toLowerCase().indexOf("igc-") == 0) {
            hasName = true;
        }
        if (value && value.i && value.i.nativeElement) {
            var nat = value.i.nativeElement;
            if (nat.___wcElement) {
                nat = nat.___wcElement;
            }
            return Loader.hasName(nat, root);
        }
        return hasName;
    }
    

    private static getClone(value: any, root: any, seen: Map<any, any>) {
        if (seen.has(value)) {
            return seen.get(value);
        }

        var isCollection = false;
        if (value && !value.___cloned && value.count !== undefined && typeof (value.count) !== 'function' && value.item) {
            isCollection = true;
            let retArr = [];
            for (let j = 0; j < value.count; j++) {
                retArr.push(Loader.getClone(value.item(j), root, seen));
            }
            value = retArr;
            return value;
        }

        if (value instanceof FormData) {
            return value;
        }

        var hasName = Loader.hasName(value, root);
        var mustPassByValue = false;
        var neverPassByValue = false;
        if (value && value._implementation && value._implementation.___mustPassByValue) {
            mustPassByValue = true;
        }
        if (value && value._implementation && value._implementation.___neverPassByValue) {
            neverPassByValue = true;
        }

        if (!Array.isArray(value) &&
            value != null && value != undefined &&
        typeof(value) == "object" && !Loader.isRoot(value, root) && (!hasName || mustPassByValue) && !value.___cloned) {

            if (!hasName && neverPassByValue) {
                return null;
            }
            //console.log(value);
        
            if (value instanceof Date) {
                return value;
            }

            var inVal = value;
            value = {};
            value.___cloned = true;
            seen.set(inVal, value);

            if (inVal.$type && inVal.$type.name == "Point") {
                value.x = inVal.x;
                value.y = inVal.y;
            } else if (!inVal.$type && !inVal._implementation &&
                inVal.x !== undefined &&
                inVal.y !== undefined) {
                value.___type = "Point";
                value.x = inVal.x;
                value.y = inVal.y;
            } else if (inVal.$type && inVal.$type.name == "Rect") {
                value.left = inVal.left;
                value.top = inVal.top;
                value.width = inVal.width;
                value.height = inVal.height;
            } else if (!inVal.$type && !inVal._implementation &&
                inVal.left !== undefined &&
                inVal.top !== undefined &&
                inVal.width !== undefined &&
                inVal.height !== undefined) {
                value.___type = "Rect";
                value.left = inVal.left;
                value.top = inVal.top;
                value.width = inVal.width;
                value.height = inVal.height;
            } else if (inVal.$type && inVal.$type.name == "Size") {
                value.width = inVal.width;
                value.height = inVal.height;
            } else if (!inVal.$type && !inVal._implementation &&
                inVal.width !== undefined &&
                inVal.height !== undefined) {
                value.___type = "Size";
                value.width = inVal.width;
                value.height = inVal.height;
            } else if (inVal.___localJson) {
                for (var copyKey of Object.keys(inVal)) {
                    value[copyKey] = inVal[copyKey];
                }
            } else {
                // looping through prototype props
                let seenProps = new Set<string>();
                for (var copyKey of getAllPropertyNames(inVal)) {
                    if (copyKey == "$type" || copyKey == "_implementation" || copyKey == "constructor" || copyKey == "seriesInternal" ||
                        copyKey == "prototype" || copyKey == "__proto__" || copyKey == "externalObject" || copyKey == "i") {
                        continue;
                    }
                    seenProps.add(copyKey);
                    if (typeof(inVal[copyKey] == "object")) {
                        value[copyKey] = Loader.getClone(inVal[copyKey], root, seen);
                    } else {
                        value[copyKey] = inVal[copyKey];
                    }
                }

                // looping through local props
                if (!inVal.$type && !inVal._implementation) {
                    // don't need to come in here for X# types since those will have been cloned above
                    for (var copyKey of Object.keys(inVal)) {
                        if (copyKey == "$type" || copyKey == "_implementation" || copyKey == "constructor" || 
                            copyKey == "prototype" || copyKey == "__proto__" || copyKey == "externalObject" || copyKey == "i") {
                            continue;
                        }
                        // ignore keys we copied previously.
                        if (!seenProps.has(copyKey)) {
                            if (typeof(inVal[copyKey] == "object")) {
                                if (document && inVal[copyKey] === document) {
                                    value[copyKey] = null;
                                } else {
                                    value[copyKey] = Loader.getClone(inVal[copyKey], root, seen);
                                }
                            } else {
                                value[copyKey] = inVal[copyKey];
                            }
                        }
                    }
                }
            }
            if (inVal.type) {
                value.type = inVal.type;               
            }
            if (inVal.$type) {
                value.$type = inVal.$type;               
            }
            if (inVal.__bounce) {
                value.__bounce = inVal.__bounce;               
            }
            if (inVal.value) {
                if (typeof(inVal[copyKey] == "object")) {
                    value.value = Loader.getClone(inVal.value, root, seen);
                } else {
                    value.value = inVal.value;
                }           
            }
            if (inVal.retType) {
                value.retType = inVal.retType;               
            }
            if (inVal.refType) {
                value.refType = inVal.refType;               
            }
            if (inVal.id) {
                value.id = inVal.id;               
            } else {
                if (Loader.hasId(inVal)) {
                    value.id = Loader.getId(inVal);
                }
            }
            if (inVal._implementation) {
                value._implementation = inVal._implementation;
            }
            // if (inVal.constructor) {
            //     value.constructor = inVal.constructor;
            // }
            if (inVal.___id) {
                value.___id = inVal.___id;
            }
        }

        return value;
    }

    private static replacerFunc(key: string, value: any, owner: any, root: any, seen: Map<any, any>) {
        var ignoreSet: Set<string> = null;

        var isCollection = false;

        if (key == "__bounce") {
            return undefined;
        }
        if (key == "_implementation") {
            return undefined;
        }
        if (key.indexOf("_") == 0 || key.indexOf("$") == 0) {
            if (key != "___id") {
                return undefined;
            }
        }

        if (owner) {
            let c = undefined;
            if (owner.$type) {
                c = owner.$type.InstanceConstructor;
            } else if (owner._implementation && owner._implementation.$type) {
                c = owner._implementation.$type.InstanceConstructor;
            }
            if (c && (c as any).__marshalByValueIgnore) {
                if (c && (c as any).__marshalByValueIgnoreSet) {
                    ignoreSet = (c as any).__marshalByValueIgnoreSet;
                } else {
                    ignoreSet = new Set<string>();
                    for (var i = 0; i < (c as any).__marshalByValueIgnore.length; i++) {
                        ignoreSet.add((c as any).__marshalByValueIgnore[i]);
                    }
                    (c as any).__marshalByValueIgnoreSet = ignoreSet;
                }
            }
        }

        if (ignoreSet && ignoreSet.has(key)) {
            return undefined;
        }

        if (value && !value.___cloned && value.count && typeof (value.count) !== 'function' && value.item) {
            isCollection = true;
            let retArr = [];
            for (let j = 0; j < value.count; j++) {
                retArr.push(Loader.getClone(value.item(j), root, seen));
            }
            value = retArr;
        }

        if (!isCollection) {
            value = Loader.getClone(value, root, seen);
        }
        
        var bounce = owner && owner.__bounce && owner.__bounce == true;

        

        if (bounce && key == "value") {
            return value;
        }

        return this.transformReturn(value, root, owner, key);
    }


    public static stringify(val: any, root: any) {
        var self = this;
        var seen = new Map<any, any>();
        function doReplace(key, value) {
            return self.replacerFunc(key, value, this, root, seen);
        }

        return JSON.stringify(val, doReplace);
    }
  
    public loadingPromise: Promise<void> = null;
    private loadingPromiseResolve: () => void = null;
    private loadingPromiseReject: () => void = null;
  
    private checkDone() {
      if (!this.isLoading) {
        if (this.loadingPromise) {
          this.loadingPromiseResolve();
          this.loadingPromise = null;
          this.loadingPromiseResolve = null;
          this.loadingPromiseReject = null;
        }
      }
    }
  
    public has(module: string) {
      return this._moduleSet.has(module);
    }
  
    public get isLoading(): boolean {
      return this._loadingSet.size > 0;
    }
  }