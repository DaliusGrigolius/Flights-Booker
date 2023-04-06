/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpContext } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { AirlineDetailsRm } from '../models/airline-details-rm';

@Injectable({
  providedIn: 'root',
})
export class AirlineService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation findAirline
   */
  static readonly FindAirlinePath = '/Airline/{airlineName}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `findAirline$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  findAirline$Plain$Response(params: {
    airlineName: string;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<AirlineDetailsRm>> {

    const rb = new RequestBuilder(this.rootUrl, AirlineService.FindAirlinePath, 'get');
    if (params) {
      rb.path('airlineName', params.airlineName, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AirlineDetailsRm>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `findAirline$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  findAirline$Plain(params: {
    airlineName: string;
  },
  context?: HttpContext

): Observable<AirlineDetailsRm> {

    return this.findAirline$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<AirlineDetailsRm>) => r.body as AirlineDetailsRm)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `findAirline()` instead.
   *
   * This method doesn't expect any request body.
   */
  findAirline$Response(params: {
    airlineName: string;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<AirlineDetailsRm>> {

    const rb = new RequestBuilder(this.rootUrl, AirlineService.FindAirlinePath, 'get');
    if (params) {
      rb.path('airlineName', params.airlineName, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AirlineDetailsRm>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `findAirline$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  findAirline(params: {
    airlineName: string;
  },
  context?: HttpContext

): Observable<AirlineDetailsRm> {

    return this.findAirline$Response(params,context).pipe(
      map((r: StrictHttpResponse<AirlineDetailsRm>) => r.body as AirlineDetailsRm)
    );
  }

}
