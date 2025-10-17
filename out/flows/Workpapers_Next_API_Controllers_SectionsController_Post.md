[web] POST /api/sections  (Workpapers.Next.API.Controllers.SectionsController.Post)  [L83–L91] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SectionLightDto [L90]
  └─ uses_service UnitOfWork
    └─ method Add [L88]
      └─ implementation UnitOfWork.Add
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SectionLightDto

