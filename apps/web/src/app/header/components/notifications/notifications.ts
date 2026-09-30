import { Component } from '@angular/core';
import { MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
@Component({
  selector: 'app-notifications',
  imports: [MatIconButton, MatIcon],
  templateUrl: './notifications.html',
  styleUrl: './notifications.css'
})
export class Notifications {
  hasUnreadNotifications = true;
}
