import { Component } from "@angular/core";
import { MatToolbar } from "@angular/material/toolbar";
import { Notifications } from "./components/notifications/notifications";
import { Logo } from "./components/logo/logo";
import { BreederBadge } from "./components/breeder-badge/breeder-badge";
@Component({
  selector: "app-header",
  imports: [MatToolbar, Notifications, Logo, BreederBadge],
  templateUrl: "./header.html",
  styleUrl: "./header.css",
})
export class Header {
  protected title = "Find my pet";
}
