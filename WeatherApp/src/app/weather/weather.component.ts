import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { WeatherService } from './weather.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss']
})
export class WeatherComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription = new Subscription();
  public form: FormGroup;
  public data: any;
  public errorMessage: string;

  constructor(
    private fb: FormBuilder,
    private weatherService: WeatherService) { }

  ngOnInit() {
    this.form = this.fb.group({
      zipCode: ['', Validators.required]
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  search = (zipCode: string): void => {
    this.data = null;
    this.errorMessage = null;

    this.subscriptions.add(this.weatherService.getWeather(zipCode).subscribe(x => {
      this.data = x;
    }, (error) => {
      this.errorMessage = 'There was an error processing your request.';
    }));
  }
}
