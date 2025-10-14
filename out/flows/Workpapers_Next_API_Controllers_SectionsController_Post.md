[web] POST /api/sections  (Workpapers.Next.API.Controllers.SectionsController.Post)  [L83–L91] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SectionLightDto [L90]
  └─ uses_service IMapper
    └─ method Map [L90]
  └─ uses_service UnitOfWork
    └─ method Add [L88]

