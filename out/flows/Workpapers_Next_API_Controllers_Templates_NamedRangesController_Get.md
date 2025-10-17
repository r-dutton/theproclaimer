[web] GET /api/named-ranges  (Workpapers.Next.API.Controllers.Templates.NamedRangesController.Get)  [L30–L47] status=200
  └─ calls NamedRangeRepository.ReadQuery [L33]
  └─ query NamedRange [L33]
    └─ reads_from NamedRanges
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ NamedRange writes=0 reads=1

