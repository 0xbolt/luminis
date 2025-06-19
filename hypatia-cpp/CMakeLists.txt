cmake_minimum_required(VERSION 3.15)
project(HypatiaCpp CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

if(DEFINED ENV{VCPKG_ROOT})
    set(CMAKE_TOOLCHAIN_FILE "$ENV{VCPKG_ROOT}/scripts/buildsystems/vcpkg.cmake"
        CACHE STRING "")
endif()

find_package(glfw3 REQUIRED)
find_package(imgui CONFIG REQUIRED)
find_package(glad CONFIG REQUIRED)

add_executable(main src/main.cpp)
# add_library(hypatia SHARED src/hypatia.cpp src/hypatia.h)
target_include_directories(main PRIVATE ${IMGUI_INCLUDE_DIRS})
target_link_libraries(main PRIVATE glad::glad glfw imgui::imgui)
