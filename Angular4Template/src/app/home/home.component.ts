import { Component, OnInit } from '@angular/core';
import {routerTransition} from '../shared/loaders/angular.animations';
import {ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [routerTransition()],
  encapsulation: ViewEncapsulation.Native
})
export class HomeComponent implements OnInit {

  public sliders: Array<any> = [];

  constructor() {
    this.sliders.push(
      {
          imagePath: 'assets/images/slider1.jpg',
          label: 'First slide label',
          text:
              'Nulla vitae elit libero, a pharetra augue mollis interdum.'
      },
      {
          imagePath: 'assets/images/slider2.jpg',
          label: 'Second slide label',
          text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'
      },
      {
          imagePath: 'assets/images/slider3.jpg',
          label: 'Third slide label',
          text:
              'Praesent commodo cursus magna, vel scelerisque nisl consectetur.'
      }
  );
  }

  ngOnInit() {
  }

}
