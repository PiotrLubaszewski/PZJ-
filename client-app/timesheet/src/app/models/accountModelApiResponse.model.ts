import { AccountModel } from './account.model';

export class AccountModelApiResponse {
  result: AccountModel;
  statusCode: number;
  error: string;
  validationErrors: string;
}
