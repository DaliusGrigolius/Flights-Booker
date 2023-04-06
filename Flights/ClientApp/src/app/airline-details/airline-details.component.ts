import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AirlineService } from './../api/services/airline.service';
import { AirlineDetailsRm } from '../api/models';

@Component({
  selector: 'app-airline-details',
  templateUrl: './airline-details.component.html',
  styleUrls: ['./airline-details.component.css']
})
export class AirlineDetailsComponent implements OnInit {

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private airlineService: AirlineService) { }

  airlineName: string = 'not loaded'
  airline: AirlineDetailsRm = {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(a => this.findAirline(a.get("airlineName")))
  }

  private findAirline = (airlineName: string | null) => {
    this.airlineName = airlineName ?? 'not passed';

    this.airlineService.findAirline({ airlineName: this.airlineName })
      .subscribe(airline => this.airline = airline)
  }

  home() {
    this.router.navigate(['/']);
  }
}
