[web] GET /api/named-ranges  (Workpapers.Next.API.Controllers.Templates.NamedRangesController.Get)  [L30–L47] status=200
  └─ calls NamedRangeRepository.ReadQuery [L33]
  └─ query NamedRange [L33]
    └─ reads_from NamedRanges
  └─ uses_service IControlledRepository<NamedRange>
    └─ method ReadQuery [L33]
      └─ ... (no implementation details available)

