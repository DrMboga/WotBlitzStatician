import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'tank-card',
  templateUrl: './tank-card.component.html',
  styleUrls: ['./tank-card.component.css']
})
export class TankCardComponent implements OnInit {

  @Input("tankInfo")
  public tankInfo: any;

  constructor() { }

  ngOnInit() {
  }

}
