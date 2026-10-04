import { Component } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
@Component({
  selector: "app-breeder-badge",
  imports: [MatButtonModule, MatIcon],
  templateUrl: "./breeder-badge.html",
  styleUrl: "./breeder-badge.css",
})
export class BreederBadge {
  test() {
    console.log("test");
  }
}
