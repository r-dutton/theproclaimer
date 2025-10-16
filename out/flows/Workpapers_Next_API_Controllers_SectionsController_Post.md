[web] POST /api/sections  (Workpapers.Next.API.Controllers.SectionsController.Post)  [L83–L91] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SectionLightDto [L90]
  └─ uses_service IMapper
    └─ method Map [L90]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L88]
      └─ ... (no implementation details available)

