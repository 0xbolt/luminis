import os
import pathlib
import pandas as pd
import numpy as np

UNITY_STREAMING_ASSETS_DIR = pathlib.Path('../luminis-unity/Assets/StreamingAssets')

def read_csv(filename: str):
    """Read a csv file inside Unity Streaming Assets folder"""
    return pd.read_csv(UNITY_STREAMING_ASSETS_DIR / filename)

def save_csv(df: pd.DataFrame, filename: str, overwrite: bool = False):
    """Save a csv file inside Unity Streaming Assets folder"""
    path = UNITY_STREAMING_ASSETS_DIR / filename
    if os.path.exists(path) and not overwrite:
        raise Exception(f"{path} already exists")
    df.to_csv(path, index=False)

def init_position_deltas(X: pd.DataFrame):
    """Randomly initialize position deltas"""
    X.d_dist = np.random.normal(0, X.sigma_dist)

def calculate_xyz(X: pd.DataFrame):
    ra = np.radians(X.ra)
    dec = np.radians(X.dec)

    proj_x = np.cos(dec) * np.cos(ra)
    proj_y = np.cos(dec) * np.sin(ra)
    proj_z = np.sin(dec)

    x = X.dist * proj_x
    y = X.dist * proj_y
    z = X.dist * proj_z
    df = pd.DataFrame({'x': x, 'y': y, 'z': z})

    dx = dy = dz = np.full_like(X.dist, np.nan)
    if 'd_dist' in X.columns:
        dx = X.d_dist * proj_x
        dy = X.d_dist * proj_y
        dz = X.d_dist * proj_z
    df = pd.concat([df, pd.DataFrame({'dx': dx, 'dy': dy, 'dz': dz})], axis=1)
    return df

def init_sigma_dist_proportional(X: pd.DataFrame, c: float = 0.01):
    """Initialize sigma_dist proportional to log dist"""
    X['sigma_dist'] = np.log(1 + X.dist) * c

def init_sigma_dist_normal(X: pd.DataFrame, mean: float, stddev: float = 0):
    """Initialize sigma_dist with a normal distribution"""
    X['sigma_dist'] = np.random.normal(mean, stddev, len(X))
