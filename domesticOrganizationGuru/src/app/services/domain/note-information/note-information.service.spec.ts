/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NoteInformationService } from './note-information.service';

describe('Service: NoteInformation', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NoteInformationService]
    });
  });

  it('should ...', inject([NoteInformationService], (service: NoteInformationService) => {
    expect(service).toBeTruthy();
  }));
});
