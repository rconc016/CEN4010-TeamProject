import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { BookFilterCommand } from '../../models/bookfiltercommand';
import { PageCommand } from '../../models/pagecommand';
import { SortCommand } from '../../models/sortcommand';
import { isNullOrUndefined } from 'util';

@Injectable({
  providedIn: 'root'
})
export class CommandService {

  constructor() { }

  /**
   * Converts the given @see{@link SortCommand} into HTTP query parameters. 
   * @param command The sorting command to convert.
   * @param params Optional parameters to append to.
   * @returns The HTTP query string parameters.
   */
  public convertSortToParams(command: SortCommand, params?: HttpParams) {
    if (isNullOrUndefined(params)) {
      params = new HttpParams();
    }

    params = this.appendStringParam(params, 'sortKey', command.key);
    params = this.appendStringParam(params, 'sortBy', command.sortBy);
    return params;
  }

  /**
   * Converts the given @see{@link BookFilterCommand} into HTTP query parameters. 
   * @param command The filter command to convert.
   * @param params Optional parameters to append to.
   * @returns The HTTP query string parameters.
   */
  public convertBookFilterToParams(command: BookFilterCommand, params?: HttpParams) {
    if (isNullOrUndefined(params)) {
      params = new HttpParams();
    }

    params = this.appendStringParam(params, 'title', command.title);
    params = this.appendStringParam(params, 'author', command.author);
    params = this.appendCurrencyParam(params, 'minPrice', command.minPrice);
    params = this.appendCurrencyParam(params, 'maxPrice', command.maxPrice);
    params = this.appendNumberParam(params, 'rating', command.rating);
    params = this.appendDateParam(params, 'minReleaseDate', command.minReleaseDate);
    params = this.appendDateParam(params, 'maxReleaseDate', command.maxReleaseDate);
    params = this.appendStringParam(params, 'genre', command.genre);
    params = this.appendBooleanParam(params, 'topSeller', command.topSeller);
    
    return params;
  }

  /**
   * Converts the given @see{@link PageCommand} into HTTP query parameters. 
   * @param command The pagination command to convert.
   * @param params Optional parameters to append to.
   * @returns The HTTP query string parameters.
   */
  public convertPageToParams(command: PageCommand, params?: HttpParams) {
    if (isNullOrUndefined(params)) {
        params = new HttpParams();
    }

    params = this.appendNumberParam(params, 'limit', command.limit);
    params = this.appendNumberParam(params, 'offset', command.offset);
    return params;
  }

  /**
   * Converts the given key and string value into
   * an HTTP query parameter. If a value is not given
   * then the parameter will not be created.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendStringParam(params: HttpParams, key: string, value?: string) {
    if (value) {
      params = this.appendParam(params, key, value);
    }

    return params;
  }

  /**
   * Converts the given key and number value into
   * an HTTP query parameter. If a value is not given
   * then the parameter will not be created.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendNumberParam(params: HttpParams, key: string, value?: number) {
    if (value) {
        params = this.appendParam(params, key, value.toString());
    }

    return params;
  }

  /**
   * Converts the given key and number value into
   * an HTTP query parameter. If a value is not given
   * then the parameter will not be created. The number
   * will be formatted in the following way: ##.##, where
   * missing values will be padded with 0.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendCurrencyParam(params: HttpParams, key: string, value?: number) {
    if (value) {
      params = this.appendParam(params, key, value.toFixed(2).padStart(5, '0'));
    }

    return params;
  }

  /**
   * Converts the given key and date value into
   * an HTTP query parameter. If a value is not given
   * then the parameter will not be created.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendDateParam(params: HttpParams, key: string, value?: Date) {
    if (value) {
        params = this.appendParam(params, key, `${value.getUTCMonth() + 1}/${value.getUTCDate()}/${value.getUTCFullYear()}`);
    }

    return params;
  }

  /**
   * Converts the given key and boolean value into
   * an HTTP query parameter. If a value is not given
   * then the parameter will not be created.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendBooleanParam(params: HttpParams, key: string, value?: boolean) {
    if (!isNullOrUndefined(value)) {
        params = this.appendParam(params, key, value.toString());
    }

    return params;
  }

  /**
   * Appends the given key and value to the
   * list of HTTP parameters.
   * @param params Optional parameters to append to.
   * @param key The key to use.
   * @param value The value to use.
   * @returns The HTTP query string parameters.
   */
  private appendParam(params: HttpParams, key: string, value: string) {
    return params.append(key, value);
  }
}
