export class CollectionResult<T> {
  totalCount: number;
  pagesCount: number;
  currentPage: number;
  data: T[];
}