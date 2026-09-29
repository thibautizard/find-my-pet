import { Component } from '@angular/core';
import { Logo } from './components/logo/logo';
@Component({
  selector: 'app-header',
  imports: [Logo],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header {
  protected title = 'Find my pet';
}
