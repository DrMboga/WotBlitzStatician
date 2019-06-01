import { Component, OnInit, Input } from '@angular/core';
import { TankStatisticDto } from '../../model/tank-statistic-dto';

@Component({
  selector: 'app-tank-card',
  templateUrl: './tank-card.component.html',
  styleUrls: ['./tank-card.component.css']
})
export class TankCardComponent implements OnInit {

  @Input()
  public tankInfo: TankStatisticDto;

  constructor() { }

  ngOnInit() {
  }

}
