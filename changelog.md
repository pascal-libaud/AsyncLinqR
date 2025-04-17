# Changelog

## 1.0.0

Initial version.

## 1.2.0

 - Add new methods:
	 - Add CrossJoinAsync (which has no equivalent in Linq)
	 - Add DefaultIfEmptyAsync
	 - Add ExceptByAsync
	 - Add ForEachAsync
	 - Add GroupByAsync
	 - Add IndexAsync
	 - Add IntersectAsync
	 - Add IntersectByAsync
	 - Add JoinAsync
	 - Add OrderAsync
	 - Add RepeatAsync
	 - Add SkipAsync
	 - Add SkipLastAsync
	 - Add SkipWhileAsync
	 - Add TakeLastAsync
	 - Add TakeWhileAsync
	 - Add ToDictionaryAsync
	 - Add ToHashSetAsync
	 - Add UnionAsync
	 - Add UnionByAsync
	 - Add ZipAsync
	 - Add ZipFullAsync (which has no equivalent in Linq)
 - Add indexes on SelectAsync
 - Add CancellationToken support on:
	 - DistinctByAsync
	 - SelectAsync
	 - SelectManyAsync
	 - SingleAsync
	 - SingleOrDefaultAsync

## 1.3.0

Add support for ConfigureAwait(false)
