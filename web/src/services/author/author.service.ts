import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CommandService } from '../../common/services/command/command.service';
import { Author } from '../../components/author/author.model';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  public readonly url = `${environment.apiUrl}/author`;

  public constructor(private http: HttpClient, private commandService: CommandService) { 
  }

  /**
   * Retrieves the information of a single author with
   * the given ID.
   * @param authorId The ID of the author to look for.
   * @returns The author if it was found.
   */
  public findById(authorId: string) {
    return this.http.get<Author>(`${this.url}/${authorId}`);
  }
}
