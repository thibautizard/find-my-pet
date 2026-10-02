import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import { Header } from "./header/header";
import { Footer } from "./footer/footer";
@Component({
  imports: [RouterModule, Header, Footer],
  selector: "app-root",
  templateUrl: "./app.html",
  styleUrl: "./app.css",
})
export class App {}
