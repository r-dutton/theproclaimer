[web] GET /workpapers/v1/matters/detailed  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersDetailedQuery)  [L90–L97] status=200
  └─ calls MatterRepository.ReadQuery [L93]
  └─ query Matter [L93]
    └─ reads_from Matters
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Matter writes=0 reads=1

