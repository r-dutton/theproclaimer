[web] GET /workpapers/v1/matters/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersDetailedQuery)  [L90–L97] status=200
  └─ calls MatterRepository.ReadQuery [L93]
  └─ query Matter [L93]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method ReadQuery [L93]
      └─ ... (no implementation details available)

