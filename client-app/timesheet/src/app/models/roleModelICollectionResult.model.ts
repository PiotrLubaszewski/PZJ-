import { RoleModel } from './roleModel.model';

export class RoleModelICollectionResult {
  data: RoleModel[];
  totalCount: number;
  pagesCount: number;
  currentPage: number;
}
