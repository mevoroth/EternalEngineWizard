#pragma once

#include "Macros/Macros.hpp"
#include "Log/Log.hpp"

namespace Eternal
{
	namespace Core
	{
		struct SystemCreateInformation;
	}
}

namespace {0}
{
	using namespace Eternal::Core;

	const Eternal::LogSystem::Log::LogCategory Log{0}("{0}");

	struct {0}Setup
	{
		static void SetupCustomSystemCreateInformation(_Inout_ SystemCreateInformation& InOutSystemCreateInformation);
	};
}
