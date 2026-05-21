import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CardComponent } from '../shared/components/card/card.component';
import { HeaderComponent } from '../header/header.component';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CardComponent, HeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {}
