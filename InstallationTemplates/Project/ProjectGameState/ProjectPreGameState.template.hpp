#pragma once

#include "Core/GameState.hpp"

namespace {0}
{
	using namespace Eternal::Core;

	class {0}PreGameState : public GameState
	{
	public:

		{0}PreGameState(_In_ Game& InGame);

		virtual void Begin() override;
		virtual void Update() override;
		virtual GameState* NextState() override;

	};
}
