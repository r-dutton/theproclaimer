[web] GET /workpapers/v1/matters/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersDetailedQuery)  [L90–L97]
  └─ calls MatterRepository.ReadQuery [L93]
  └─ queries Matter [L93]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method ReadQuery [L93]

