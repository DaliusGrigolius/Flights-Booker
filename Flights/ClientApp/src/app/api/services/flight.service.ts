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

import { BookDto } from '../models/book-dto';
import { FlightRm } from '../models/flight-rm';

@Injectable({
  providedIn: 'root',
})
export class FlightService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation searchFlight
   */
  static readonly SearchFlightPath = '/Flight';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchFlight$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlight$Plain$Response(params?: {
    fromDate?: string;
    toDate?: string;
    from?: string;
    destination?: string;
    airline?: string;
    numberOfPassengers?: number;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<Array<FlightRm>>> {

    const rb = new RequestBuilder(this.rootUrl, FlightService.SearchFlightPath, 'get');
    if (params) {
      rb.query('fromDate', params.fromDate, {});
      rb.query('toDate', params.toDate, {});
      rb.query('from', params.from, {});
      rb.query('destination', params.destination, {});
      rb.query('airline', params.airline, {});
      rb.query('numberOfPassengers', params.numberOfPassengers, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchFlight$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlight$Plain(params?: {
    fromDate?: string;
    toDate?: string;
    from?: string;
    destination?: string;
    airline?: string;
    numberOfPassengers?: number;
  },
  context?: HttpContext

): Observable<Array<FlightRm>> {

    return this.searchFlight$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<Array<FlightRm>>) => r.body as Array<FlightRm>)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchFlight()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlight$Response(params?: {
    fromDate?: string;
    toDate?: string;
    from?: string;
    destination?: string;
    airline?: string;
    numberOfPassengers?: number;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<Array<FlightRm>>> {

    const rb = new RequestBuilder(this.rootUrl, FlightService.SearchFlightPath, 'get');
    if (params) {
      rb.query('fromDate', params.fromDate, {});
      rb.query('toDate', params.toDate, {});
      rb.query('from', params.from, {});
      rb.query('destination', params.destination, {});
      rb.query('airline', params.airline, {});
      rb.query('numberOfPassengers', params.numberOfPassengers, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchFlight$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchFlight(params?: {
    fromDate?: string;
    toDate?: string;
    from?: string;
    destination?: string;
    airline?: string;
    numberOfPassengers?: number;
  },
  context?: HttpContext

): Observable<Array<FlightRm>> {

    return this.searchFlight$Response(params,context).pipe(
      map((r: StrictHttpResponse<Array<FlightRm>>) => r.body as Array<FlightRm>)
    );
  }

  /**
   * Path part for operation bookFlight
   */
  static readonly BookFlightPath = '/Flight';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `bookFlight()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  bookFlight$Response(params?: {
    body?: BookDto
  },
  context?: HttpContext

): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, FlightService.BookFlightPath, 'post');
    if (params) {
      rb.body(params.body, 'application/*+json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `bookFlight$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  bookFlight(params?: {
    body?: BookDto
  },
  context?: HttpContext

): Observable<void> {

    return this.bookFlight$Response(params,context).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation findFlight
   */
  static readonly FindFlightPath = '/Flight/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `findFlight$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  findFlight$Plain$Response(params: {
    id: string;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<FlightRm>> {

    const rb = new RequestBuilder(this.rootUrl, FlightService.FindFlightPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<FlightRm>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `findFlight$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  findFlight$Plain(params: {
    id: string;
  },
  context?: HttpContext

): Observable<FlightRm> {

    return this.findFlight$Plain$Response(params,context).pipe(
      map((r: StrictHttpResponse<FlightRm>) => r.body as FlightRm)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `findFlight()` instead.
   *
   * This method doesn't expect any request body.
   */
  findFlight$Response(params: {
    id: string;
  },
  context?: HttpContext

): Observable<StrictHttpResponse<FlightRm>> {

    const rb = new RequestBuilder(this.rootUrl, FlightService.FindFlightPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json',
      context: context
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<FlightRm>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `findFlight$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  findFlight(params: {
    id: string;
  },
  context?: HttpContext

): Observable<FlightRm> {

    return this.findFlight$Response(params,context).pipe(
      map((r: StrictHttpResponse<FlightRm>) => r.body as FlightRm)
    );
  }

}
