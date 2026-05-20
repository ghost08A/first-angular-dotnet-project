import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CardComponent } from "../shared/components/card/card.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, CardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
