import { Injectable, Inject } from '@angular/core';
import { WebapiRequestsService } from '../../shared/services/webapi-requests.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TankStatisticDto } from '../../model/tank-statistic-dto';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class AccountTanksService extends WebapiRequestsService {

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) { super(http, baseUrl); }

  getTanksDataByQuery(dataQuery: string, isAccountLoggedin: boolean): Observable<TankStatisticDto[]> {
    return this.http.get<any>(`${this.baseUrl}${dataQuery}`).pipe(
      map(d => d.value as TankStatisticDto[]),
      catchError(this.handleError)
    );
  }

}
