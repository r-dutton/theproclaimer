[web] GET /api/named-ranges  (Workpapers.Next.API.Controllers.Templates.NamedRangesController.Get)  [L30–L47]
  └─ calls NamedRangeRepository.ReadQuery [L33]
  └─ queries NamedRange [L33]
    └─ reads_from NamedRanges
  └─ uses_service IControlledRepository<NamedRange>
    └─ method ReadQuery [L33]

