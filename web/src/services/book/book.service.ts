import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CommandService } from '../../common/services/command/command.service';
import { SortCommand } from '../../common/models/sortcommand';
import { BookFilterCommand } from '../../common/models/bookfiltercommand';
import { PageCommand } from '../../common/models/pagecommand';
import { BookInterface } from '../../components/book.details/book.interface';
import { BookDescription } from '../../components/book.details/book.description.model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  public readonly url = `${environment.apiUrl}/book`;

  public constructor(private http: HttpClient, private commandService: CommandService) { 
  }

  /**
   * Retrieves all the books from the API.
   * The result can be modified by appliying
   * sorting, filtering and pagination commands.
   * @param sortCommand The command which defines how to sort the result.
   * @param filterCommand The command which defines how to filter the result.
   * @param pageCommand The command which defines how to limit the result.
   * @returns An observable object containing the result.
   */
  public findAll(sortCommand: SortCommand, filterCommand: BookFilterCommand, pageCommand: PageCommand) {
    var params = new HttpParams();

    if (sortCommand) {
      params = this.commandService.convertSortToParams(sortCommand, params);
    }

    if (filterCommand) {
      params = this.commandService.convertBookFilterToParams(filterCommand, params);
    }

    if (pageCommand) {
      params = this.commandService.convertPageToParams(pageCommand, params);
    }

    let options = {
      params: params
    };

    return this.http.get<BookInterface[]>(this.url, options);
  }

  /**
   * Retrieves the information of a single book with
   * the given ID.
   * @param bookId The ID of the book to look for.
   * @returns The book if it was found.
   */
  public findById(bookId: string) {
    return this.http.get<BookInterface>(`${this.url}/${bookId}`);
  }
  
  /**
   * Retreives the description of a single book.
   * @param descriptionId The ID of the book description to look for.
   */
  public findDescriptionById(descriptionId: string) {
    return this.http.get<BookDescription>(`${this.url}/description/${descriptionId}`);
  }
}
