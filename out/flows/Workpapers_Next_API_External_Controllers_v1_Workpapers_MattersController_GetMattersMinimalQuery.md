[web] GET /workpapers/v1/matters/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersMinimalQuery)  [L71–L78]
  └─ calls MatterRepository.ReadQuery [L74]
  └─ queries Matter [L74]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method ReadQuery [L74]

