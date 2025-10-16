[web] GET /workpapers/v1/matters/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.MattersController.GetMattersMinimalQuery)  [L71–L78] status=200
  └─ calls MatterRepository.ReadQuery [L74]
  └─ query Matter [L74]
    └─ reads_from Matters
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Matter writes=0 reads=1

