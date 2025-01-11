import { Component } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';

@Component({
  selector: 'app-automenu',
  templateUrl: './automenu.component.html',
  styleUrl: './automenu.component.css'
})
export class AutomenuComponent {
  timedOutCloser: any;

  constructor() { this.timedOutCloser = setTimeout(() => { }, 1) }

  mouseEnter(trigger: MatMenuTrigger) {
    if (this.timedOutCloser) {
      clearTimeout(this.timedOutCloser);
    }
    trigger.openMenu();
  }

  mouseLeave(trigger: MatMenuTrigger) {
    this.timedOutCloser = setTimeout(() => {
      trigger.closeMenu();
    }, 100);
  }
}
