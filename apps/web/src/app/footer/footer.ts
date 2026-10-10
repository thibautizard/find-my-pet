import { Component } from "@angular/core";
import { MatIcon } from "@angular/material/icon";

@Component({
  selector: "app-footer",
  imports: [MatIcon],
  templateUrl: "./footer.html",
  styleUrl: "./footer.css",
})
export class Footer {
  protected title = "Find my pet SAS";
  currentYear = new Date().getFullYear();
}
