import { AccountModelICollectionResult } from './accountModelCollectionResult.model';

export class AccountModelICollectionResultApiResponse {
  result: AccountModelICollectionResult;
  statusCode: number;
  error: string;
  validationErrors: string;
}

