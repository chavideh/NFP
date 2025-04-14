import { Component } from '@angular/core';
import {SwitcherComponent} from '../shared/switcher/switcher.component';
import {LoaderComponent} from '../shared/loader/loader.component';
import {HeaderComponent} from '../shared/header/header.component';
import {SidebarComponent} from '../shared/sidebar/sidebar.component';
import {RouterOutlet} from '@angular/router';
import {RsidebarComponent} from '../shared/rsidebar/rsidebar.component';

@Component({
  selector: 'app-layout',
  imports: [
    SwitcherComponent,
    LoaderComponent,
    HeaderComponent,
    SidebarComponent,
    RouterOutlet,
    RsidebarComponent
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

}
