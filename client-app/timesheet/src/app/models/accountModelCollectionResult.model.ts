import { AccountModel } from './account.model';

export class AccountModelICollectionResult {
  data: AccountModel[];
  totalCount: number;
  pagesCount: number;
  currentPage: number;
}
