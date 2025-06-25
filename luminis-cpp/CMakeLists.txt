# Minimum CMake version and project
cmake_minimum_required(VERSION 3.15)
project(LuminisCpp CXX)

# C++ standard
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Toolchain (vcpkg)
if(DEFINED ENV{VCPKG_ROOT})
    set(CMAKE_TOOLCHAIN_FILE "$ENV{VCPKG_ROOT}/scripts/buildsystems/vcpkg.cmake" CACHE STRING "")
endif()

# Dependencies
find_package(glfw3 REQUIRED)
find_package(imgui CONFIG REQUIRED)
find_package(glad CONFIG REQUIRED)

# Targets
add_executable(main src/main.cpp)
add_library(luminis SHARED src/luminis.cpp)

# Includes and linking
# luminis library includes
target_include_directories(luminis PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/src)
# main executable links
target_link_libraries(main PRIVATE glad::glad glfw imgui::imgui)

# Post-build: Copy and rename library for Unity
add_custom_command(TARGET luminis POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy
    $<TARGET_FILE:luminis>
    ${CMAKE_SOURCE_DIR}/../luminis-unity/Assets/Plugins/LuminisCpp$<TARGET_FILE_SUFFIX:luminis>
)
