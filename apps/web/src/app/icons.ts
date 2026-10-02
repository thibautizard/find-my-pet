import { EnvironmentProviders, inject, provideAppInitializer } from "@angular/core";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { mdiBellOutline, mdiPaw } from "@mdi/js";

// Register MDI icons here; use them with <mat-icon svgIcon="name" />
const icons = {
  paw: mdiPaw,
  bellOutline: mdiBellOutline,
};

export function provideMdiIcons(): EnvironmentProviders {
  return provideAppInitializer(() => {
    const registry = inject(MatIconRegistry);
    const sanitizer = inject(DomSanitizer);
    for (const [name, path] of Object.entries(icons)) {
      registry.addSvgIconLiteral(
        name,
        sanitizer.bypassSecurityTrustHtml(`<svg viewBox="0 0 24 24"><path d="${path}"/></svg>`),
      );
    }
  });
}
